using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CubeController : MonoBehaviour
{

    public ScoreSystem scoreSystem;
    public static CubeController instance;
    public GridCoordinate coord = new GridCoordinate(0, 0);

    private EventAction animationAction;
    private float animationTimer = 1.0f;
    private TransformData previousTransform;
 
    void Start() {
      instance = this;
      previousTransform = transform.Clone();
      previousTransform.localPosition -= Vector3.forward;
    }

    void Update() {
        float old = animationTimer;
        animationTimer += Time.deltaTime;
        animationTimer = Mathf.Min(animationTimer, 0.08f);
        float progress = 1.5f - 0.5f * (animationTimer / 0.08f);
        Vector3 offset = previousTransform.localPosition;
        Vector3 rotationAxis = Vector3.zero;
        switch(animationAction) {
            case EventAction.Up:
                offset = Vector3.forward;
                rotationAxis = Vector3.right;
                break;
            case EventAction.Down:
                offset = Vector3.back;
                rotationAxis = Vector3.left;
                break;
            case EventAction.Right:
                offset = Vector3.right;
                rotationAxis = Vector3.back;
                break;
            case EventAction.Left:
                offset = Vector3.left;
                rotationAxis = Vector3.forward;
                break;
        }
        transform.localPosition = previousTransform.localPosition + progress * offset;
        transform.localRotation = Quaternion.AngleAxis(90.0f * progress, rotationAxis) * previousTransform.localRotation;
    }

    public void StepUp()
    {
        this.Step(EventAction.Up);
        scoreSystem.onInput(EventAction.Up);
     }

    public void StepDown()
    {
        this.Step(EventAction.Down);
        scoreSystem.onInput(EventAction.Down);
    }

    public void StepLeft()
    {
        this.Step(EventAction.Left);
        scoreSystem.onInput(EventAction.Left);
    }

    public void StepRight()
    {
        this.Step(EventAction.Right);
    }

    public void Step(EventAction input) {
        coord.Offset(input);
        scoreSystem.onInput(input);
        animationAction = input;
        animationTimer = 0.0f;
        previousTransform = transform.Clone();
    }



}
