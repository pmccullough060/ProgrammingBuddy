import * as monaco from "monaco-editor";
import { Component, Inject, OnInit } from "@angular/core";
import { Subscription } from "rxjs/internal/Subscription";
import { ICompilationResponseModel } from "src/app/models/compilationResponseModel";
import { ICompileRequestModel } from "src/app/models/compileRequestModel";
import { IDiagnosticResponseModel } from "src/app/models/diagnosticResponseModel";
import { CompilerService } from "src/app/services/compiler.service";
import { DOCUMENT } from "@angular/common";
import { Position } from "monaco-editor";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: 'app-editor',
    templateUrl: './editor.component.html',
    styleUrls: ['./editor.component.scss'],
})
export class TextEditorComponent {
    form: FormGroup;

    private subscriptions: { [key: string ]: Subscription } = {}

    constructor(private compilerService: CompilerService, 
                        @Inject(DOCUMENT) document: Document, 
                        private fb: FormBuilder) {
        
        // Create the reactive form:
        this.form = this.fb.group({
            projectName: ['', [Validators.required]]
        })       
    }
    
    // Monaco props:
    editor: any;
    decorations: any = [];
    markers: any = [];
    editorOptions = { theme: 'vs-dark', language: 'csharp', fontSize: "18px" };
    code: string = 'public class Program \n{\n    public static void Main()\n    { \n    } \n}';
    
    // Diagnostic response models:
    diagnosticsResponseModel: IDiagnosticResponseModel[] = []

    ngOnInit(): void {}

    compile(): void {

        // Create the request model:
        const model : ICompileRequestModel = {
            code: this.code, 
            language: "csharp"
        }

        // Send the C# code to the backend to be compiled:
        this.subscriptions.compile = this.compilerService.compileCode(model).subscribe({next: (result) => {
            this.processResult(result);
        }})
    }

    saveProject() : void {

        // Check if the form has errors:
        if(!this.form.valid){
            return;
        }

        // Get the project name:
        const projectName = this.form.value.projectName;
        
        

        console.log("here rahheheeh");
    }

    processResult(response: ICompilationResponseModel){
        
        // Update the diagnostics response model property:
        this.diagnosticsResponseModel = response.diagnosticResponseModels ?? []
        
        // Highlight any errors to the user:
        this.displayErrors();
    }

    displayErrors(){
        
        console.log(this.decorations);

        // Clear any existing errors:
        this.clearErrors();

        // Add the errors from the API:
        this.diagnosticsResponseModel.forEach(error => {
            this.formatError(error);
        });
    }

    // Clear the errors to show the new ones:
    clearErrors(){
        this.editor.deltaDecorations(this.decorations, [{ range: {
            startLineNumber: 1,
            startColumn: 1,
            endColumn: 1,
            endLineNumber: 1
        }, options : { } }]);
    }

    // Showing errors to the user:
    formatError(error : IDiagnosticResponseModel){
        this.decorations = this.editor.deltaDecorations(
            this.decorations,
            [
                {
                    range: {
                        startLineNumber: error.position.line,
                        startColumn: error.position.start,
                        endColumn: error.position.end,
                        endLineNumber: error.position.line
                    },
                    options: {
                        isWholeLine: true,
                        inlineClassName: 'myInlineDecoration'
                    }
                },
            ]
        )
    }

    // Get the editor instance:
    editorInit(editor: any) {
        this.editor = editor;

        (window as any).monaco.languages.registerHoverProvider('csharp', {
            provideHover: (model: monaco.editor.ITextModel , position: Position) => {
                
            // Here we map the position to the error messages:
            // Get the first error message with the same lines:
            let diagnosticModel = this.diagnosticsResponseModel.find(model => model.position.line === position.lineNumber);
            
            // If the hover position doesn't correspond to an diagnostic objects - return null:
            if(diagnosticModel == null){
                return {
                    range: null,
                    contents: null
                };
            }

            return {
                range: new monaco.Range(
                    diagnosticModel.position.line,
                    1,
                    999, //We deal with errors on a per line basis
                    diagnosticModel.position.line)
                ,
                contents: [
                    { value: '**Error**' },
                    { value: diagnosticModel.message },
                    { value: diagnosticModel.url }
                ]
            };
            }
          });
    }

    typeAtCursor(text: string){
        this.editor.trigger('keyboard', 'type', {text: text});
    }

    // Ideally should be storing these in a .json config file:
    addVariable(){
        let text = 'int i = 0; //Assigning a value to a variable'
        this.typeAtCursor(text);
    }

    addConditional(){
        let text = 'bool firstCondition = true;\n\nbool secondCondition = true;\n\nif(firstCondition) \n{\n// Evaluated igf firstCondition is true\n}\nelse if(secondCondition) \n{\n// evaluated if secondCondition is true\n}\nelse\n{\n// Evaluated only if none of the proceeding statements are true\n}\n'
        this.typeAtCursor(text);
    }
}