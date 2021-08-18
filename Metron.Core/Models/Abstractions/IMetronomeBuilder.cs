namespace Metron
{
    public interface IMetronomeBuilder
    {
        IMetronomeBuilder withTimer(ITimer timerImplementor);
        IMetronomeBuilder withSound(IMetromomeSound soundImplementor);
        IMetronomeBuilder withColor(IColor colorImplementor);
        IMetronomeBuilder withPlatformSpecificXMLDoc(IPlatformSpecificXMLDoc xmlDocImplementor);
        IMetronomeBuilder withHighLimit(int metronomeHighLimit);
        IMetronomeBuilder withLowLimit(int metronomeLowLimit);
        IMetronomeModel Build();
    }
}