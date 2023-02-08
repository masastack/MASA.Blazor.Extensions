function initialize(id, options) {
    return monaco.editor.create(document.getElementById(id), options);
}
function getValue(instance) {
    return instance.getValue();
}
function setValue(instance, value) {
    instance.setValue(value);
}
function setTheme(theme) {
    monaco.editor.setTheme(theme);
}
function getModels() {
    return monaco.editor.getModels();
}
function getModel(instance) {
    return instance.getModel();
}
function setModelLanguage(instance, languageId) {
    monaco.editor.setModelLanguage(instance.getModel(), languageId);
}
function remeasureFonts() {
    monaco.editor.remeasureFonts();
}
function addKeybindingRules(rules) {
    monaco.editor.addKeybindingRules(rules);
}
function addKeybindingRule(rule) {
    monaco.editor.addKeybindingRule(rule);
}
function registerCompletionItemProvider(value) {
    value.forEach((v) => {
        monaco.languages.registerCompletionItemProvider(v.language, {
            provideCompletionItems: function (model, position) {
                return {
                    suggestions: v.suggestions
                };
            }, triggerCharacters: v.triggerCharacters
        });
    });
}
export { initialize, getValue, setValue, setTheme, getModels, getModel, setModelLanguage, remeasureFonts, addKeybindingRules, addKeybindingRule, registerCompletionItemProvider };
//# sourceMappingURL=monaco.editor.js.map