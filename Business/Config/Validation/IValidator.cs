using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Config.Validation
{
    public interface IValidator
    {
        void Validate(IValidatorModel validatorModel);
    }

    public class Validator : IValidator
    {
        public void Validate(IValidatorModel validatorModel)
        {
            validatorModel.ValidateModel();
        }
    }
}
