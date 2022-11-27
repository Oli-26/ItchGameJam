using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MobController : Node
{
    public IIntent Intent { get; private set; }
    public bool IsAggressive => Intent is ChasePlayerIntent;

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }

    public void OfferIntent(IIntent intent)
    {
        if (intent == null)
        {
            throw new ArgumentException("cannot offer null intent");
        }
        if (Intent == null
            || intent.Priority > Intent.Priority)
        {
            Intent = intent;
        }
    }

    public void ClearIntent(int upToPriotity = int.MaxValue)
    {
        if (Intent != null
            && Intent.Priority <= upToPriotity)
        {
            Intent = null;
        }
    }
}
