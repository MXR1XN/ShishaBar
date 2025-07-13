using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Waiter : MonoBehaviour
{
	[SerializeField] LayerMask clickableLayers;
	[SerializeField] LayerMask interactableLayers;
	[SerializeField] private GameObject interactiveUI;
    [SerializeField] private GameObject interactiveUI2;
    [SerializeField] private GameObject interactiveUI3;
    private Vector3 targetPosition;

	private NewCustomActions input;
	private Rigidbody body;
	private NavMeshAgent agent;
	private Animator animator;
	private IInteractible interactiveSeat;

    private void Awake()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		input = new NewCustomActions();
		AssignInputs();
	}
	private void Start()
	{
		targetPosition = agent.transform.position;
	}

	private void AssignInputs()
	{
		input.Touch.Mouse.performed += ctx => ClickToMove(ctx);
		input.Touch.TouchInput.performed += ctx => ClickToMove(ctx);	
		input.Touch.Touchscreen.performed += ctx => ClickToMove(ctx);
	}

	private void RotateTowards(Vector3 targetPosition)
	{
		Vector3 direction = (targetPosition - transform.position).normalized;
		if (direction == Vector3.zero)
			return;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
	}

	private void MoveWatier()
	{
		RotateTowards(targetPosition);
		agent.SetDestination(targetPosition);
		animator.SetBool(WaiterAnimationNames.ISWALKING, true);
	}

	public void ClickToMove(InputAction.CallbackContext callbackContext)
	{
		if (interactiveUI.activeSelf)
		{
			return;
		}

        if (interactiveUI2.activeSelf)
        {
            return;
        }
        if (interactiveUI3.activeSelf)
        {
            return;
        }


        RaycastHit hit;
		
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, interactableLayers)) 
		{
			if (interactiveSeat != null) 
			{
				interactiveSeat.IsUIInitiator = false;
			}
			interactiveSeat = hit.collider.GetComponent<IInteractible>();
			targetPosition = hit.point;
			MoveWatier();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IInteractible interactible)) 
		{
			if (interactible == interactiveSeat) 
			{
				interactiveSeat.UpdateUI();
				interactiveSeat.ShowUI();
			}
        }
	}

	private void Update()
	{
		float distance = Vector3.Distance(transform.position, targetPosition);
		if (agent.remainingDistance <= 0.3f)
		{
			animator.SetBool(WaiterAnimationNames.ISWALKING, false);
		}
	}
	private void OnEnable() => input.Enable(); 
	private void OnDisable() => input.Disable();

	public static class WaiterAnimationNames 
	{
		public const string ISWALKING = "isWalking";
	}
}
