using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cable : MonoBehaviour
{
    [SerializeField] private Transform aTransform = null;
    [SerializeField] private Transform bTransform = null;

    private LineRenderer line = null;

    public Transform ATransform
    {
        get => aTransform;
        set => aTransform = value;
    }

    public Transform BTransform
    {
        get => bTransform;
        set => bTransform = value;
    }

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    private void Start()
    {
        if (aTransform == null || bTransform == null)
        {
            Debug.LogError("one of the target transforms has not been set up, this is not permitted");
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        line.SetPosition(0, aTransform.position);
        line.SetPosition(1, bTransform.position);
    }
}
