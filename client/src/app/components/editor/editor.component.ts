import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs/internal/Subscription";
import { ICompileRequestModel } from "src/app/models/compileRequestModel";
import { CompilerService } from "src/app/services/compiler.service";

@Component({
    selector: 'app-editor',
    templateUrl: './editor.component.html',
    styleUrls: ['./editor.component.scss'],
})
export class EditorComponent implements OnInit {
    constructor(private compilerService: CompilerService) {}

    private subscriptions: { [key: string ]: Subscription } = {}

    // temp props:
    editorOptions = { theme: 'vs-dark', language: 'csharp', fontSize: "18px" };
    code: string = 'public class Program \n{\n    public static void Main()\n    { \n    } \n}';
    
    ngOnInit(): void {}

    compile(): void {

        const model : ICompileRequestModel = {
            code: this.code, 
            language: "csharp"
        }

        this.subscriptions.compile = this.compilerService.compileCode(model).subscribe({next: (result) => {
            console.log(result);
        }})
    }

}