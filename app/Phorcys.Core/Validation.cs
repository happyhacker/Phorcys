using NHibernate.Validator.Engine;

namespace Phorcys.Core
{
	public static class Validation
	{
		///<remarks>
		/// The output of this function should be either put into your IoC container or cached somewhere to prevent
		/// re-reading of the config files.
		///</remarks>
		public static ValidatorEngine CreateValidationEngine()
		{
			var validator = new ValidatorEngine();
			validator.Configure();
			
			return validator;
		}
	}
}