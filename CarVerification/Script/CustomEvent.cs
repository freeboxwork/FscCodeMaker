using System;

public static class CustomEvent
{
    public static Action<string> evnFscChange;

    public static void Add_FSC_VIEW_Event(Action<string> action)
    {
        evnFscChange += action;
    }

}


