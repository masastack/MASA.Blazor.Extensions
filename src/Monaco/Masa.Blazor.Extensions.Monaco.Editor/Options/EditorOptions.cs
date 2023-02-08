namespace Masa.Blazor;

public class EditorOptions
{
    /// <summary>
    /// The initial Language of the auto created model in the editor.
    /// To not automatically create a model, use `model: null`.
    /// </summary>
    public string Language { get; set; } = "html";

    /// <summary>
    /// This editor is used inside a diff editor.
    /// </summary>
    public bool InDiffEditor { get; set; }

    /// <summary>
    /// The aria label for the editor's textarea (when it is focused).
    /// </summary>
    public string AriaLabel { get; set; }

    /// <summary>
    ///  The `tabindex` property of the editor's textarea
    /// </summary>
    public int TabIndex { get; set; }

    /// <summary>
    /// 在指定的列上渲染垂直线。
    /// 默认为空数组。
    /// </summary>
    public int[] Rulers { get; set; }

    /// <summary>
    /// A string containing the word separators used when doing word navigation.
    /// Defaults to `~!@#$%^&*()-=+[{]}\\|;:\'",.<>/?
    /// </summary>
    public string WordSeparators { get; set; } = "`~!@#$%^&*()-=+[{]}\\|;:\\'\",.<>/?";

    /// <summary>
    /// Enable Linux primary clipboard.
    /// Defaults to true.
    /// </summary>
    public bool SelectionClipboard { get; set; } = true;

    /// <summary>
    ///  Accept suggestions on provider defined characters.
    ///  Defaults to true.
    /// </summary>
    public bool AcceptSuggestionOnCommitCharacter { get; set; } = true;

    /// <summary>
    /// Accept suggestions on ENTER.
    /// Defaults to 'on'.
    /// 'on' | 'smart' | 'off'
    /// </summary>
    public string AcceptSuggestionOnEnter { get; set; } = "on";

    /// <summary>
    /// Controls the minimal number of visible leading and trailing lines surrounding the cursor.
    /// Defaults to 0.
    /// </summary>
    public int CursorSurroundingLines { get; set; }

    /// <summary>
    /// Controls when `cursorSurroundingLines` should be enforced
    /// Defaults to `default`, `cursorSurroundingLines` is not enforced when cursor position is changed
    /// by mouse.
    /// </summary>
    public string CursorSurroundingLinesStyle { get; set; } = "default";

    /// <summary>
    /// Render last line number when the file ends with a newline.
    /// Defaults to true.
    /// </summary>
    public bool RenderFinalNewline { get; set; }

    /// <summary>
    /// Remove unusual line terminators like LINE SEPARATOR (LS), PARAGRAPH SEPARATOR (PS).
    /// Defaults to 'prompt'.
    /// </summary>
    public string UnusualLineTerminators { get; set; }

    /// <summary>
    /// An URL to open when Ctrl+H (Windows and Linux) or Cmd+H (OSX) is pressed in
    /// the accessibility help dialog in the editor.
    /// Defaults to "https://go.microsoft.com/fwlink/?linkid=852450"
    /// </summary>
    public string AccessibilityHelpUrl { get; set; } = "https://go.microsoft.com/fwlink/?linkid=852450";

    /// <summary>
    /// Configure the editor's accessibility support.
    /// Defaults to 'auto'. It is best to leave this to 'auto'.
    /// </summary>
    public string AccessibilitySupport { get; set; } = "auto";

    /// <summary>
    /// Should the corresponding line be selected when clicking on the line number?
    /// Defaults to true.
    /// </summary>
    public bool SelectOnLineNumbers { get; set; } = true;

    /// <summary>
    /// Control the width of line numbers, by reserving horizontal space for rendering at least an amount of digits.
    /// Defaults to 5.
    /// </summary>
    public int LineNumbersMinChars { get; set; } = 5;

    /// <summary>
    /// Enable the rendering of the glyph margin.
    /// Defaults to true in vscode and to false in monaco-editor.
    /// </summary>
    public bool GlyphMargin { get; set; }

    /// <summary>
    /// The width reserved for line decorations (in px).
    /// Line decorations are placed between line numbers and the editor content.
    /// You can pass in a string in the format floating point followed by "ch". e.g. 1.3ch.
    /// Defaults to 10.
    /// </summary>
    public string LineDecorationsWidth { get; set; } = "10";

    /// <summary>
    /// When revealing the cursor, a virtual padding (px) is added to the cursor, turning it into a rectangle.
    /// This virtual padding ensures that the cursor gets revealed before hitting the edge of the viewport.
    /// Defaults to 30 (px).
    /// </summary>
    public int RevealHorizontalRightPadding { get; set; } = 30;

    /// <summary>
    /// Options for auto closing brackets.
    /// Defaults to Language defined behavior.
    /// 'always' | 'languageDefined' | 'beforeWhitespace' | 'never'
    /// </summary>
    public string AutoClosingBrackets { get; set; } = "languageDefined";

    /// <summary>
    ///  Options for auto closing quotes.
    ///  Defaults to Language defined behavior.
    ///  'always' | 'languageDefined' | 'beforeWhitespace' | 'never'
    /// </summary>
    public string AutoClosingQuotes { get; set; } = "languageDefined";

    /// <summary>
    /// Controls whether the editor should automatically adjust the indentation when users type, paste, move or indent lines.
    /// Defaults to advanced.
    ///  'none' | 'keep' | 'brackets' | 'advanced' | 'full'
    /// </summary>
    public string AutoIndent { get; set; } = "none";

    /// <summary>
    /// Options for auto surrounding.
    /// Defaults to always allowing auto surrounding.
    /// 'languageDefined' | 'quotes' | 'brackets' | 'never'
    /// </summary>
    public string AutoSurround { get; set; } = "languageDefined";

    /// <summary>
    /// Enable that the editor will install a ResizeObserver to check if its container dom node size has changed.
    /// Defaults to false.
    /// </summary>
    public bool AutomaticLayout { get; set; }

    /// <summary>
    /// Timeout for running code actions on save.
    /// </summary>
    public int CodeActionsOnSaveTimeout { get; set; }

    /// <summary>
    /// Show code lens
    /// Defaults to true.
    /// </summary>
    public bool CodeLens { get; set; } = true;

    /// <summary>
    ///  Enable inline color decorators and color picker rendering.
    /// </summary>
    public bool ColorDecorators { get; set; }

    /// <summary>
    /// Enable custom contextmenu.
    /// Defaults to true.
    /// </summary>
    public bool Contextmenu { get; set; } = true;

    /// <summary>
    /// Syntax highlighting is copied.
    /// </summary>
    public bool CopyWithSyntaxHighlighting { get; set; }

    /// <summary>
    /// Control the cursor animation style, possible values are 'blink', 'smooth', 'phase', 'expand' and 'solid'.
    /// Defaults to 'blink'.
    /// </summary>
    public string CursorBlinking { get; set; } = "blink";

    /// <summary>
    /// Control the cursor style, either 'block' or 'line'.
    /// Defaults to 'line'.
    /// 'line' | 'block' | 'underline' | 'line-thin' | 'block-outline' | 'underline-thin'
    /// </summary>
    public string CursorStyle { get; set; } = "line";

    /// <summary>
    ///  Control the width of the cursor when cursorStyle is set to 'line'
    /// </summary>
    public int CursorWidth { get; set; } = 0;

    /// <summary>
    /// Disable the use of `transform: translate3d(0px, 0px, 0px)` for the editor margin and lines layers.
    /// The usage of `transform: translate3d(0px, 0px, 0px)` acts as a hint for browsers to create an extra layer.
    /// Defaults to false.
    /// </summary>
    public bool DisableLayerHinting { get; set; }

    /// <summary>
    /// Disable the optimizations for monospace fonts.
    /// Defaults to false.
    /// </summary>
    public bool DisableMonospaceOptimizations { get; set; }

    /// <summary>
    /// Controls if the editor should allow to move selections via drag and drop.
    /// Defaults to false.
    /// </summary>
    public bool DragAndDrop { get; set; }

    /// <summary>
    /// Copying without a selection copies the current line.
    /// </summary>
    public bool EmptySelectionClipboard { get; set; }

    /// <summary>
    /// 要添加到编辑器的类名。
    /// </summary>
    public string ExtraEditorClassName { get; set; }

    /// <summary>
    /// 控制find小部件的行为。
    /// </summary>
    public EditorFindOptions Find { get; set; } = new();

    /// <summary>
    /// 显示溢出小部件为“已修复”。
    /// 默认为' false '。
    /// </summary>
    public bool FixedOverflowWidgets { get; set; }

    /// <summary>
    /// 启用代码折叠。
    /// 默认为true。
    /// </summary>
    public bool Folding { get; set; } = true;

    /// <summary>
    /// 选择折叠策略。'auto'使用为当前文档贡献的策略，'indentation'使用
    /// 基于折叠策略。
    /// 默认为'auto'。
    /// </summary>
    public string FoldingStrategy { get; set; } = "auto";

    /// <summary>
    /// 字体系列
    /// </summary>
    public string FontFamily { get; set; }

    /// <summary>
    /// 启用字体连接。
    /// 默认为false。
    /// </summary>
    public bool FontLigatures { get; set; }

    /// <summary>
    /// 字体粗细
    /// </summary>
    public int FontSize { get; set; }

    /// <summary>
    /// The font weight
    /// </summary>
    public string FontWeight { get; set; } = "normal";

    /// <summary>
    /// 启用粘贴格式。
    /// 默认为false。
    /// </summary>
    public bool FormatOnPaste { get; set; }

    /// <summary>
    /// 在类型上启用格式。
    /// 默认为false。
    /// </summary>
    public bool FormatOnType { get; set; }

    /// <summary>
    /// 游标是否隐藏在概览标尺中。
    /// 默认为false。
    /// </summary>
    public bool HideCursorInOverviewRuler { get; set; }

    /// <summary>
    /// 配置编辑器的悬停。
    /// </summary>
    public EditorHoverOptions Hover { get; set; } = new();

    /// <summary>
    /// 用于渲染的初始主题。
    /// 当前可用的开箱即用主题是:'vs'(默认)，'vs-dark'， 'hc-black'， 'hc-light '。
    /// 你可以通过' monaco.editor.defineTheme '创建自定义主题。
    /// 切换主题，使用' monaco.editor.setTheme '。
    /// 如果操作系统处于高对比度模式，主题可能会被覆盖，除非' autoDetectHighContrast '设置为false。
    /// </summary>
    public string Theme { get; set; } = "vs-dark";

    /// <summary>
    /// 编辑器中自动创建的模型的初始值。
    /// 不自动创建模型，使用' model: null '。
    /// </summary>
    public string? Value { get; set; }


    /// <summary>
    /// 字母间距
    /// </summary>
    public int LetterSpacing { get; set; }

    /// <summary>
    /// 控制小地图的行为和渲染。
    /// </summary>
    public EditorMinimapOptions Minimap { get; set; } = new();

    /// <summary>
    /// 建议小部件的行高。
    /// 默认为编辑器行高。
    /// </summary>
    public int LineHeight { get; set; }

    /// <summary>
    /// 启用检测链接，使他们可点击。
    /// 默认为true。
    /// </summary>
    public bool Links { get; set; } = true;

    /// <summary>
    /// Enable highlighting of matching brackets.
    /// Defaults to 'always'.
    ///  'never' | 'near' | 'always'
    /// </summary>
    public string MatchBrackets { get; set; } = "always";

    /// <summary>
    ///  一个乘数用于' deltaX '和' delay '的鼠标滚轮滚动事件。
    /// 默认为1。
    /// </summary>
    public int MouseWheelScrollSensitivity { get; set; } = 1;

    /// <summary>
    /// 在按住Ctrl的同时使用鼠标滚轮，可以缩放编辑器中的字体。
    /// 默认为false。
    /// </summary>
    public bool MouseWheelZoom { get; set; }

    /// <summary>
    /// 合并重叠的选择。
    /// 默认为true
    /// </summary>
    public bool MultiCursorMergeOverlapping { get; set; } = true;

    /// <summary>
    /// 用于用鼠标添加多个游标的修饰符。
    /// 默认为'alt'
    /// </summary>
    public string MultiCursorModifier { get; set; } = "alt";

    /// <summary>
    /// 启用语义事件突出显示。
    /// 默认为true。
    /// </summary>
    public bool OccurrencesHighlight { get; set; } = true;

    /// <summary>
    /// 控件是否应该在概览标尺周围绘制边框
    /// 默认为' true '。
    /// </summary>
    public bool OverviewRulerBorder { get; set; } = true;

    /// <summary>
    /// 概览标尺应呈现的垂直车道数。
    /// 默认为3。
    /// </summary>
    public int OverviewRulerLanes { get; set; } = 3;

    /// <summary>
    ///参数提示选项。
    /// </summary>
    public EditorParameterHintOptions EditorParameterHints { get; set; } = new();

    /// <summary>
    /// 启用快速建议(影子建议)
    /// 默认为true。
    /// </summary>
    public bool QuickSuggestions { get; set; } = true;

    /// <summary>
    /// 快速提示显示延迟(毫秒)
    /// 默认为10 (ms)
    /// </summary>
    public int QuickSuggestionsDelay { get; set; } = 500;

    /// <summary>
    /// Should the editor be read only. See also `domReadOnly`.
    /// Defaults to false.
    /// </summary>
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Enable rendering of control characters.
    /// Defaults to true.
    /// </summary>
    public bool RenderControlCharacters { get; set; }

    /// <summary>
    /// Render +/- indicators for added/deleted changes.
    /// Defaults to true.
    /// </summary>
    public bool RenderIndicators { get; set; } = true;

    /// <summary>
    /// Enable rendering of current line highlight.
    /// Defaults to all.
    /// </summary>
    public string RenderLineHighlight { get; set; } = "all";

    /// <summary>
    /// Enable rendering of whitespace.
    /// Defaults to 'selection'.
    /// </summary>
    public string RenderWhitespace { get; set; } = "none";

    /// <summary>
    /// Render the editor selection with rounded borders.
    /// Defaults to true.
    /// </summary>
    public bool RoundedSelection { get; set; } = true;

    /// <summary>
    /// 启用滚动可以超出最后一列若干列。
    /// 默认为5。
    /// </summary>
    public int ScrollBeyondLastColumn { get; set; } = 5;

    /// <summary>
    /// Enable that scrolling can go one screen size after the last line.
    /// Defaults to true.
    /// </summary>
    public bool ScrollBeyondLastLine { get; set; } = true;

    /// <summary>
    /// 控制滚动条的行为和呈现。
    /// </summary>
    public EditorScrollbarOptions EditorScrollbar { get; set; } = new();

    /// <summary>
    /// Enable selection highlight.
    /// Defaults to true.
    /// </summary>
    public bool SelectionHighlight { get; set; } = true;

    /// <summary>
    /// Controls whether the fold actions in the gutter stay always visible or hide unless the mouse is over the gutter.
    /// Defaults to 'mouseover'.
    /// </summary>
    public string ShowFoldingControls { get; set; } = "mouseover";

    /// <summary>
    /// Controls fading out of unused variables.
    /// </summary>
    public bool ShowUnused { get; set; }

    /// <summary>
    /// Enable that the editor animates scrolling to a position.
    /// Defaults to false.
    /// </summary>
    public bool SmoothScrolling { get; set; }

    /// <summary>
    /// Enable snippet suggestions. Default to 'true'.
    /// </summary>
    public string SnippetSuggestions { get; set; } = "true";

    /// <summary>
    /// Performance guard: Stop rendering a line after x characters.
    /// Defaults to 10000.
    /// Use -1 to never stop rendering
    /// </summary>
    public int StopRenderingLineAfter { get; set; } = 10000;

    /// <summary>
    ///  Suggest options.
    /// </summary>
    public SuggestOptions Suggest { get; set; } = new SuggestOptions();

    /// <summary>
    /// The font size for the suggest widget.
    /// Defaults to the editor font size.
    /// </summary>
    public int SuggestFontSize { get; set; }

    /// <summary>
    /// The line height for the suggest widget.
    /// Defaults to the editor line height.
    /// </summary>
    public int SuggestLineHeight { get; set; }

    /// <summary>
    /// Enable the suggestion box to pop-up on trigger characters.
    /// Defaults to true.
    /// </summary>
    public bool SuggestOnTriggerCharacters { get; set; } = true;

    /// <summary>
    ///  The history mode for suggestions.
    /// </summary>
    public string SuggestSelection { get; set; }

    /// <summary>
    /// Enable tab completion.
    /// </summary>
    public string TabCompletion { get; set; }


    /// <summary>
    ///  Inserting and deleting whitespace follows tab stops.
    /// </summary>
    public bool UseTabStops { get; set; }

    /// <summary>
    /// Controls whether completions should be computed based on words in the document.
    /// Defaults to true.
    /// </summary>
    public bool WordBasedSuggestions { get; set; } = true;

    /// <summary>
    /// Control the wrapping of the editor.
    /// When `wordWrap` = "off", the lines will never wrap.
    /// When `wordWrap` = "on", the lines will wrap at the viewport width.
    /// When `wordWrap` = "wordWrapColumn", the lines will wrap at `wordWrapColumn`.
    /// When `wordWrap` = "bounded", the lines will wrap at min(viewport width, wordWrapColumn).
    /// Defaults to "off".
    /// </summary>
    public string WordWrap { get; set; } = "off";

    /// <summary>
    /// Configure word wrapping characters. A break will be introduced after these characters.
    /// </summary>
    public string WordWrapBreakAfterCharacters { get; set; } = " \t})]?|&,;";

    /// <summary>
    /// 配置换行字符。在这些角色之前会有一段休息。
    /// </summary>
    public string WordWrapBreakBeforeCharacters { get; set; } = "{([+";

    /// <summary>
    /// Control the wrapping of the editor.
    /// When `wordWrap` = "off", the lines will never wrap.
    /// When `wordWrap` = "on", the lines will wrap at the viewport width.
    /// When `wordWrap` = "wordWrapColumn", the lines will wrap at `wordWrapColumn`.
    /// When `wordWrap` = "bounded", the lines will wrap at min(viewport width, wordWrapColumn).
    /// Defaults to 80.
    /// </summary>
    public int WordWrapColumn { get; set; } = 80;

    /// <summary>
    /// 控制换行线的缩进。可以是:'none'， 'same'， 'indent'或'deepIndent'。
    /// 在vscode中默认为'same'，在monaco-editor中默认为'none'
    /// </summary>
    public string WrappingIndent { get; set; } = "none";
}