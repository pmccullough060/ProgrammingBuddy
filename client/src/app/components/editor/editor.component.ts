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
    editorOptions = { theme: 'vs-dark', language: 'csharp' };
    code: string = 'function x() {\nconsole.log("hello world");\n';
    
    ngOnInit(): void {}

    compile(): void {

        const model : ICompileRequestModel = {
            code: this.code, 
            language: "csharp"
        }

        this.subscriptions.compile = this.compilerService.compileCode(model).subscribe({next: (result: string) => {
            console.log(result);
        }})
    }

}