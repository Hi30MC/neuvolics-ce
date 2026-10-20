namespace NeuvolicsUtil;

internal struct CataclysmState
{
    public CataclysmState()
    {
        state = 0b011;
    }

    private int state;

    public int ZephironCount
    {
        readonly get
        {
            return state & 0b111;
        }
        set
        {
            state &= ~0b111;
            state |= (value & 0b111);
        }
    }
}
