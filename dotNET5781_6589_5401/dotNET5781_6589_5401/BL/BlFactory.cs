namespace BLAPI
{
    public static class BlFactory
    {
        public static IBL GetBl() => BL.BLIMP.Instance;
    }
}
