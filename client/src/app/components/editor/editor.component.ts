import * as monaco from "monaco-editor";
import { Component, ElementRef, Inject, OnInit, ViewChild } from "@angular/core";
import { Subscription } from "rxjs/internal/Subscription";
import { ICompilationResponseModel } from "src/app/models/compilationResponseModel";
import { ICompileRequestModel } from "src/app/models/compileRequestModel";
import { IDiagnosticResponseModel } from "src/app/models/diagnosticResponseModel";
import { CompilerService } from "src/app/services/compiler.service";
import { DOCUMENT } from "@angular/common";

@Component({
    selector: 'app-editor',
    templateUrl: './editor.component.html',
    styleUrls: ['./editor.component.scss'],
})
export class TextEditorComponent implements OnInit {

    constructor(private compilerService: CompilerService, @Inject(DOCUMENT) document: Document) {}

    private subscriptions: { [key: string ]: Subscription } = {}

    // Monaco props:
    editor: any;
    decorations: any = [];
    markers: any = [];
    editorOptions = { theme: 'vs-dark', language: 'csharp', fontSize: "18px" };
    code: string = 'public class Program \n{\n    public static void Main()\n    { \n    } \n}';
    
    // Very rough but this is working :)
    ngOnInit(): void {
        monaco.editor.create(document.getElementById('container')!, {
            value: '\n\nHover over this text',
            language: 'csharp'
        });

        this.hoverProvider();
    }

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

    processResult(response: ICompilationResponseModel){
        // Highlight any errors to the user:
        this.displayErrors(response.diagnosticResponseModels ?? []);

        this.hoverProvider();
    }

    displayErrors(errors: IDiagnosticResponseModel[]){
        
        console.log(this.decorations);

        // Clear any existing errors:
        this.clearErrors();

        // Add the errors from the API:
        errors.forEach(error => {
            this.formatError(error);
        });
    }

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
                        isWholeLine: false,
                        inlineClassName: 'myInlineDecoration'
                    }
                },
            ]
        )
    }

    hoverProvider(){

        console.log("here");

        monaco.languages.registerHoverProvider('csharp', {
            provideHover: function(model: any, position: any) { 
                // Log the current word in the console, you probably want to do something else here.
                
                console.log(model);

                console.log(position);
                
                return {
                    range: new monaco.Range(
                        1,
                        1,
                        10,
                        10)
                    ,
                    contents: [
                        { value: '**SOURCE**' },
                        { value: 'Hello' }
                    ]
                };
            }
        });
    }

    // Get the editor instance:
    editorInit(editor: any) {
        this.editor = editor;

        this.hoverProvider();
    }
}