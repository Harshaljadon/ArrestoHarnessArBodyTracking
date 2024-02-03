using UnityEngine;
using System.Collections;

public class HarnessSizeColliderHandTrigger : MonoBehaviour
{
    //SetClipPlanes setClipPlanes;

    public float scalingDuration = 2.0f; // Duration of the scaling animation in seconds

    public bool  rightHandEnter, bothHandEnetered, swapDone;
    HarnessMasserManagerUI harnessMasserManagerUI;
    public PassHarnessToHumanBodyTracking passHarnessToHumanBodyTracking;

    public int countIndex;
    //public int currentSizeIndex;


    public Transform leftShoulderPos, rightHandPos ;


    public float leftShoulderRightHandDistandXAxis, exitEnterXAxisDis;

    public HarnessSorting harnessSorting;

    

    public Vector2 enterPoint, nextFrameEnterPoint, directionHand;
    public float angleRightDarection;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("RightHand"))
        {
            rightHandEnter = true;
            rightHandPos = other.gameObject.transform;
            enterPoint = new Vector2(rightHandPos.position.x, rightHandPos.position.y);
            StartCoroutine(nameof(NextFrameHandPos));

        }


    }

    IEnumerator NextFrameHandPos()
    {
        yield return new WaitForSeconds(.1f);
        if (rightHandPos != null)
        {
            nextFrameEnterPoint = new Vector2(rightHandPos.transform.position.x, rightHandPos.transform.position.y);
            if (nextFrameEnterPoint != enterPoint)
            {
                directionHand = (nextFrameEnterPoint - enterPoint).normalized;
                angleRightDarection = Vector2.Angle(directionHand, Vector2.right);

            }
            if (rightHandEnter)
            {


                if (!swapDone && angleRightDarection < 50)
                {


                    swapDone = true;
                    passHarnessToHumanBodyTracking.ChangeHarness();
                    //HarnessSizeIndex();
                }
            }
        }


    }

    void HarnessSizeIndex()
    {
        if (rightHandEnter)
        {
            switch (harnessSorting.harnessSizetype)
            {

                case HarnessSizetype.twoTypeSize:
                    if (countIndex == 1)
                    {
                        countIndex = 0;
                        ChangeHarnessSize(1); // medium size
                    }
                    else
                    {
                        countIndex++;
                        ChangeHarnessSize(3); // xl size

                    }
                    break;
                case HarnessSizetype.threeTypeSize:
                    if (countIndex == 2)
                    {
                        countIndex = 0;

                    }
                    else
                    {
                    countIndex++;

                    }

  
                    switch (countIndex)
                    {
                        case 0:
                            // small
                            ChangeHarnessSize(0);

                            break;
                        case 1:
                            // large
                            ChangeHarnessSize(2);

                            break;
                        case 2:
                            // xl
                            ChangeHarnessSize(3);
                            break;
                    }

                    break;
                case HarnessSizetype.universal:
                    if (countIndex < 5)
                    {

                        if (countIndex == 4)
                        {
                            countIndex = 0;
                            ChangeHarnessSize(countIndex);
                        }
                        else
                        {
                            countIndex++;
                            ChangeHarnessSize(countIndex);

                        }
                    }
                    break;
            }
           
            bothHandEnetered = true;
        }
    }

    void ChangeHarnessSize(int id)
    {
        if (harnessMasserManagerUI != null && !harnessMasserManagerUI.takingSnap)
        {
            SceneManag.Instance.harnessSize = (HarnessSize)id;

            harnessMasserManagerUI.AdjustHarnessSizeHandInteraction(id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand"))
        {
            swapDone = false;
            rightHandEnter = false;

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        passHarnessToHumanBodyTracking = FindAnyObjectByType<PassHarnessToHumanBodyTracking>();
        //harnessMasserManagerUI = FindObjectOfType<HarnessMasserManagerUI>();
        //countIndex = (int)SceneManag.Instance.harnessSize;
        //if (SceneManager.GetActiveScene().buildIndex == 4)
        //{
        //    harnessSorting = FindObjectOfType<HarnessSorting>();
        //}
    }

}
