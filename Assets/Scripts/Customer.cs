using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{

	public event Action<Customer> ShishaOrdered;
	public event Action<Customer> ShishaRecived;

	private Transform exitPosition;
	public IAvailablePlace targetChair;
	private Animator animator;
	private NavMeshAgent agent;
	private Rigidbody rigidBody;
	public Shisha shisha;
	public SeatHolder SeatHolder;
	public bool IsOrderAccepted;
	public int id;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = 0.5f;
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		exitPosition = GameObject.FindWithTag("Exit").transform;
		shisha = new Shisha();
		targetChair = SeatHolder.GetAvailable(this);
		if (targetChair != null)
		{
			MoveToChair();
		}
	}

	private void Update()
	{
		bool condition =
		targetChair != null
		&& agent.enabled;

		if (condition)
		{		
			float distance = Vector3.Distance(targetChair.Transform.position, transform.position);
			if (distance < 3.5f)
			{
				transform.position = targetChair.SeatPoint.position;
				animator.SetBool("isWalking", false);
				SitOnChair();
			}
		}
	}

	private void MoveToChair()
	{
		agent.SetDestination(targetChair.Transform.position);
		animator.SetBool("isWalking", true);
	}

	private IEnumerator DeleteCustomers()
	{
		float waitTime = 5f;
		yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
	}

	public void MoveToExit()
	{
		StartCoroutine(DeleteCustomers());
	}

	private void RotateTowards(Vector3 targetPosition)
	{
		Vector3 direction = (targetPosition - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void SitOnChair()
	{
		transform.position = targetChair.SeatPoint.position;
		agent.enabled = false;
		animator.SetBool("isSitten", true);
		SitAndFaceInstantly();
		ShishaOrdered?.Invoke(this);
	}
	private void SitAndFaceInstantly()
	{
		var seat = targetChair.Transform.GetComponentInParent<Seat>();
		var lookPointTransform = targetChair.LookPoint == null ? seat.transform : targetChair.LookPoint;
		Vector3 directionToFace = lookPointTransform.position - transform.position;
		directionToFace.y = 0;
		if (directionToFace.magnitude > 0)
		{
			Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
			transform.rotation = targetRotation;
		}
		
	}

	public Shisha GetShishaOrder()
	{
		return shisha;
	}
}
