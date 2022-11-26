import { IPositionModel } from "./PositionModel";

export interface IDiagnosticResponseModel {
    message: string;
    position: IPositionModel;
}