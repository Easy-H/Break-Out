using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] GameObject[] created = null;

    [SerializeField] Transform[] createPos = null;

    [SerializeField] float createTime = 2;
    [SerializeField] protected float spendTime = 0;

    [SerializeField] protected int goalCreateCount = 1;

    [SerializeField] AudioSource createSound;

    private void Start()
    {
        createSound = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        AddTime();

        if (spendTime < createTime)
            return;

        spendTime -= createTime;
        Create();

    }

    protected virtual void AddTime() {
        spendTime += Time.deltaTime * PhaseManager.level;
    }

    protected virtual void Create() {
        for (int i = 0, j = 0; i < createPos.Length && j < goalCreateCount; i++)
        {

            if (goalCreateCount - j == createPos.Length - i || Random.Range(0, createPos.Length) < goalCreateCount)
            {
                Instantiate(created[Random.Range(0, created.Length)], createPos[i].position, Quaternion.identity);
                j++;

            }

        }

        if (createSound)
            createSound.Play();
    }

}
