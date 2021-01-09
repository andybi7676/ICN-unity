using UnityEngine;
using UnityEngine.Events;
using System;

public class ProgressBar : FillBar {

    public static ProgressBar instance;

    public CPB coalSlider;
    public CPB waterSlider;
    public CPB metalSlider;
    private float coal;
    private float water;
    private float metal;

    // Event to invoke when the progress bar fills up
    private UnityEvent onProgressComplete;

    // Create a property to handle the slider's value
    public new float CurrentValue {
        get {
            return base.CurrentValue;
        }
        set {
            // If the value exceeds the max fill, invoke the completion function
            if (value >= slider.maxValue)
            {
                onProgressComplete.Invoke();
                OnProgressComplete();
            }

            // Remove any overfill (i.e. 105% fill -> 5% fill)
            base.CurrentValue = value % slider.maxValue;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start () {
        // Initialize onProgressComplete and set a basic callback
        if (onProgressComplete == null)
            onProgressComplete = new UnityEvent();
        onProgressComplete.AddListener(OnProgressComplete);
    }

    void Update () {
        water = waterSlider.GetAmount();
        coal = coalSlider.GetAmount();
        metal = metalSlider.GetAmount();
        // Maybe change to divide 3 not take minnimum
        CurrentValue = (water + coal + metal)/3;
    }

    // The method to call when the progress bar fills up
    void OnProgressComplete() {
        Debug.Log("Progress Complete");
    }
}
