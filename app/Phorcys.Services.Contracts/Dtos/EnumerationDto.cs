using Gcc.Architecture.Core.Interfaces;

namespace Phorcys.Services.Contracts.Dtos
{
    /// <summary>
    /// An enumeration dto.  The dto is passed over the service boundary and is considered part of the contract by which data is transferred between the UI
    /// and service layer.  These objects will typically consist of properties and an Equals(object o) and GetHashCode() method only.
    /// </summary>
    public class EnumerationDto : ValidatableDto
    {
        public string Name { get; set; }

        #region Equals and GetHashCode

        public override bool Equals(object obj)
        {
            EnumerationDto enumerationDto = obj as EnumerationDto;
            if (enumerationDto == null)
                return false;

            if (Name != enumerationDto.Name)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = (hash*397) ^ Name.GetHashCode();
                
                return hash;
            }
        }

        #endregion Equals and GetHashCode
    }
}
