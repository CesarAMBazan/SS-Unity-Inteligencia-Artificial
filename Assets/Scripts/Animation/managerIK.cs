using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerIK : MonoBehaviour
{
    [SerializeField]
    private Boolean shouldOnlyLookWhenTalking;
    private Animator _animator;
    private static readonly int IsTalking = Animator.StringToHash("IsTalking");

    public bool ikActive;

    public Transform objTarget;

    public float lookWeight;

    // dummy pivot
    private GameObject objPivot;

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        
        
        // Dummy pivot
        objPivot = new GameObject("DummyPivot");
        objPivot.transform.SetParent(transform);
        objPivot.transform.localPosition = new Vector3(0, 1.677f, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!objTarget)
        {
            objTarget = GameObject.Find("PlayerCamera").GetComponent<Transform>();
        }

        bool shouldTalk = !shouldOnlyLookWhenTalking || _animator.GetBool(IsTalking);
        objPivot.transform.LookAt(objTarget);
        float pivotRotY = objPivot.transform.localRotation.y;
        // Debug.Log(pivotRotY);
        // target distance
        float dist = Vector3.Distance(objPivot.transform.position, objTarget.position);
        if (shouldTalk)
        {
            if (pivotRotY is < 0.65f and > -0.65f && dist < 3.6f)
            {
                // Target Tracking
                lookWeight = Mathf.Lerp(lookWeight, 1, Time.deltaTime * 2f);
            }
        }
        // Target release
        lookWeight = Mathf.Lerp(lookWeight, 0, Time.deltaTime * 2f);
    }

    private void OnAnimatorIK()
    {
        if (!_animator || !ikActive) return;
        if (objTarget == null) return;
        
        _animator.SetLookAtWeight(lookWeight);
        _animator.SetLookAtPosition(objTarget.position);
    }
}
