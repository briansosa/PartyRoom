using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Contracts
{
    public interface IValidator
    {
        void Validate(IValidatorModel validatorModel);
    }

    public interface IValidatorModel
    {
        void ValidateModel();
    }

    public class Validator : IValidator
    {
        public void Validate(IValidatorModel validatorModel)
        {
            validatorModel.ValidateModel();
        }
    }
}
