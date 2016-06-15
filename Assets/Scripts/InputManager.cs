using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {

    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    public KeyCode jump = KeyCode.Space;
    public KeyCode attack1 = KeyCode.F;
    public KeyCode attack2 = KeyCode.R;

    public Dictionary<Inputs, InputConfig> inputs = new Dictionary<Inputs, InputConfig>();

    void Start()
    {
        this.inputs[Inputs.VERTICAL] = new InputConfig(this.up, this.down);
        this.inputs[Inputs.HORIZONTAL] = new InputConfig(this.right, this.left);
        this.inputs[Inputs.JUMP] = new InputConfig(this.jump);
        this.inputs[Inputs.ATTACK1] = new InputConfig(this.attack1);
        this.inputs[Inputs.ATTACK2] = new InputConfig(this.attack2);
    }

    void FixedUpdate()
    {
        foreach (InputConfig input in this.inputs.Values)
        {
            input.Calculate();
        }
    }

    public InputActions GetInput(Inputs input)
    {
        return this.inputs[input].inputAction;
    }

    public float GetValue(Inputs input)
    {
        return this.inputs[input].value;
    }

}

public enum Inputs
{
    HORIZONTAL, VERTICAL, JUMP, ATTACK1, ATTACK2
}

public enum InputActions
{
        NONE, RELEASED, HOLD, PRESSED
}

public class InputConfig
{
    public KeyCode positiveInput;
    public KeyCode negativeInput;
    public bool hasNegativeInput;
    public bool calculated;
    public InputActions inputAction;
    public float value;

    public InputConfig(KeyCode positiveInput)
    {
        this.positiveInput = positiveInput;
        this.negativeInput = KeyCode.None;
        this.hasNegativeInput = false;
        this.calculated = false;
        this.inputAction = InputActions.NONE;
        this.value = 0f;
    }

    public InputConfig(KeyCode positiveInput, KeyCode negativeInput)
    {
        this.positiveInput = positiveInput;
        this.negativeInput = negativeInput;
        this.hasNegativeInput = true;
        this.calculated = false;
        this.inputAction = InputActions.NONE;
        this.value = 0f;
    }

    private bool GetInput(bool positive, bool negative)
    {
        if (positive)
        {
            this.value = 1f;
        }
        if (negative)
        {
            this.value = -1f;
        }
        if (positive && negative)
        {
            this.value = 0f;
            return false;
        }
        return positive || negative;
    }

    public void Calculate()
    {

        //PRESS
        if (this.GetInput(Input.GetKeyDown(this.positiveInput), this.hasNegativeInput ? Input.GetKeyDown(this.negativeInput) : false))
        {
            this.inputAction = InputActions.PRESSED;
        }
        //HOLD
        else if (this.GetInput(Input.GetKey(this.positiveInput), this.hasNegativeInput ? Input.GetKey(this.negativeInput) : false))
        {
            this.inputAction = InputActions.HOLD;
        }
        //RELEASE
        else if (this.GetInput(Input.GetKeyUp(this.positiveInput), this.hasNegativeInput ? Input.GetKeyUp(this.negativeInput) : false))
        {
            this.inputAction = InputActions.RELEASED;
        }
        //NONE
        else
        {
            this.value = 0f;
            this.inputAction = InputActions.NONE;
        }
    }

}

