</> C#


using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Bindings.ImGui;

namespace KeyboundBrawlerHelper;

public sealed class Plugin : IDalamudPlugin
{
    public string Name => "Keybound Brawler Helper";

    private readonly WindowSystem _windowSystem = new("KeyboundBrawlerHelper");
    private readonly HelperWindow _window;

    public Plugin(IDalamudPluginInterface pluginInterface)
    {
        _window = new HelperWindow();
        _windowSystem.AddWindow(_window);

        pluginInterface.UiBuilder.Draw += _windowSystem.Draw;
        pluginInterface.UiBuilder.OpenConfigUi += OpenWindow;
    }

    private void OpenWindow() => _window.IsOpen = true;

    public void Dispose()
    {
        _windowSystem.RemoveAllWindows();
    }
}

public sealed class HelperWindow : Dalamud.Interface.Windowing.Window
{
    private string _word = "";

    public HelperWindow() : base("Keybound Brawler Helper")
    {
        SizeConstraints = new()
        {
            MinimumSize = new(350, 150),
            MaximumSize = new(600, 400)
        };
    }

    public override void Draw()
    {
        ImGui.TextWrapped("Enter an English word from Keybound Brawler.");
        ImGui.InputText("Word", ref _word, 256);

        if (ImGui.Button("Copy word"))
            ImGui.SetClipboardText(_word);

        ImGui.SameLine();

        if (ImGui.Button("Clear"))
            _word = "";
    }
}
