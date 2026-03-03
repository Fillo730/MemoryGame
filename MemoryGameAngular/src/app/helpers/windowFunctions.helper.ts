import { isWindowHistoryValid } from "../utils/window-guart.util";

export function windowGoBack() : void {
    if(isWindowHistoryValid()) {
        window.history.back();
    }
}