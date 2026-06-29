using VM = OctoType.ViewModels.Exercices;

namespace OctoType.MVVM.Views;

public partial class ExerciceGeneratorView : ContentPage
{
	public ExerciceGeneratorView(VM.ExerciceGeneratorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private static void InsertTextInEditor(string text, Editor edito)
    {
        string currentText = edito.Text ?? string.Empty;

        int cursorPosition = edito.CursorPosition;

        string textToInsert = text;

        string newText = currentText.Insert(cursorPosition, textToInsert);

        edito.Text = newText;

        edito.CursorPosition = cursorPosition + textToInsert.Length;
    }

    private void MenuFlyoutItem_AddEnterMarkClicked(object sender, EventArgs e)
    {
        InsertTextInEditor("↵", EditorGenerateText);
    }

    private void MenuFlyoutItem_AddTabMarkClicked(object sender, EventArgs e)
    {
        InsertTextInEditor("⟶", EditorGenerateText);
    }
}