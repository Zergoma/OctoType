namespace XyloType.Behaviors;

public partial class DigitsOnlyBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.TextChanged += OnTextChanged;
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is not Entry entry)
        {
            return;
        }

        string filtered = new(
            (e.NewTextValue ?? string.Empty)
                .Where(char.IsDigit)
                .ToArray());

        if (entry.Text != filtered)
        {
            entry.Text = filtered;
        }
    }
}
