interface Monaco {
    editor: any;
    languages:any
}

declare const monaco: Monaco;

function initialize(id: string, options: any) {
    return monaco.editor.create(document.getElementById(id), options);
}

function getValue(instance: any) {
    return instance.getValue();
}
function setValue(instance: any, value:string) {
    instance.setValue(value);
}
function setTheme(theme: string) {
    monaco.editor.setTheme(theme);
}

function getModels() {
    return monaco.editor.getModels();
}

function getModel(instance: any) {
    return instance.getModel();
}

function setModelLanguage(instance: any, languageId: string) {
    monaco.editor.setModelLanguage(instance.getModel(), languageId);
}
function remeasureFonts() {
    monaco.editor.remeasureFonts();
}

function addKeybindingRules(rules: any) {
    monaco.editor.addKeybindingRules(rules);
}

function addKeybindingRule(rule: any) {
    monaco.editor.addKeybindingRule(rule);
}

function registerCompletionItemProvider(value: any[]) {
    value.forEach((v) => {
        monaco.languages.registerCompletionItemProvider(v.language, {
            provideCompletionItems: function (model, position) {
                return {
                    suggestions: v.suggestions
                };
            }, triggerCharacters: v.triggerCharacters
        });
    })
}

export {
    initialize,
    getValue,
    setValue,
    setTheme,
    getModels,
    getModel,
    setModelLanguage,
    remeasureFonts,
    addKeybindingRules,
    addKeybindingRule,
    registerCompletionItemProvider
}