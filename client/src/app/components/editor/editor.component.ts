import { Component, OnInit } from "@angular/core";
import { EditorComponent } from "ngx-monaco-editor";
import { Subscription } from "rxjs/internal/Subscription";
import { ICompilationResponseModel } from "src/app/models/compilationResponseModel";
import { ICompileRequestModel } from "src/app/models/compileRequestModel";
import { IDiagnosticResponseModel } from "src/app/models/diagnosticResponseModel";
import { CompilerService } from "src/app/services/compiler.service";

@Component({
    selector: 'app-editor',
    templateUrl: './editor.component.html',
    styleUrls: ['./editor.component.scss'],
})
export class TextEditorComponent implements OnInit {
    constructor(private compilerService: CompilerService) {}

    private subscriptions: { [key: string ]: Subscription } = {}

    // Monaco props:
    editor: any;
    editorOptions = { theme: 'vs-dark', language: 'csharp', fontSize: "18px" };
    code: string = 'public class Program \n{\n    public static void Main()\n    { \n    } \n}';
    
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

    processResult(response: ICompilationResponseModel){
        
        // Highlight any errors to the user:
        this.displayErrors(response.diagnosticResponseModels ?? []);
    }

    displayErrors(errors: IDiagnosticResponseModel[]){
        errors.forEach(error => {
            this.formatError(error);
        });
    }

    formatError(error : IDiagnosticResponseModel){
        this.editor.deltaDecorations(
            [],
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

    editorInit(editor: any) {

        this.editor = editor;

        // Here you can access editor instance
        console.log("loaded", editor);
    }

}