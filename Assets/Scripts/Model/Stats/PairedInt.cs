﻿using UnityEngine;
using System.Collections;

/**
 * This class represents a pair of integer values
 */
public abstract class PairedInt {
    int trueValue; //Actual value of attribute
    int falseValue; //Changable value, can be affected in combat

    public bool isMaxed() {
        return trueValue <= falseValue;
    }

    public void setBoth(int both) {
        setTrue(both);
        setFalse(both);
    }

    public void addBoth(int both) {
        addTrue(both);
        addFalse(both);
    }

    public void subtractBoth(int both) {
        subtractTrue(both);
        subtractFalse(both);
    }

    public void setBothPercentage(float percentage) {
        setTruePercentage(percentage);
        setFalsePercentage(percentage);
    }

    public virtual void setTrue(int trueValue) {
        this.trueValue = trueValue;
    }

    public void setTruePercentage(float percentage) {
        setTrue((int)(trueValue * percentage));
    }

    public int getTrue() {
        return trueValue;
    }

    public void addTrue(int amount) {
        setTrue(trueValue + amount);
    }

    public void subtractTrue(int amount) {
        setTrue(trueValue - amount);
    }

    public virtual void setFalse(int falseValue) {
        this.falseValue = falseValue;
    }

    public void setFalsePercentage(float percentage) {
        setFalse((int)(falseValue * percentage));
    }

    public int getFalse() {
        return falseValue;
    }

    public void addFalse(int amount) {
        setFalse(falseValue + amount);
    }

    public void subtractFalse(int amount) {
        setFalse(falseValue - amount);
    }

    public float getRatio() {
        return ((float)falseValue) / trueValue;
    }

    /**
     * Reset falseValue to be trueValue
     */
    public void reset() {
        setFalse(trueValue);
    }

    /**
     * resetValue = trueValue * percentage
     * set falseValue to resetValue if
     * it's greater, otherwise, keep falseValue
     */
    public void reset(float percentage) {
        int resetValue = (int)(percentage * trueValue);
        setFalse(Mathf.Max(resetValue, falseValue));
    }
}
