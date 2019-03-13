namespace Lab
{
    public interface IValidator<in T>
    {
        bool Validate(T model);
    }
}