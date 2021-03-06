namespace Coypu.Actions
{
    internal class Select : DriverAction
    {
        private readonly DriverScope scope;
        private readonly string locator;
        private readonly string option;

        internal Select(Driver driver, DriverScope scope, string locator, string option, Options timingOptions)
            : base(driver, timingOptions)
        {
            this.scope = scope;
            this.locator = locator;
            this.option = option;
        }

        public override void Act()
        {
            Driver.Select(Driver.FindField(locator, scope),option);
        }
    }
}