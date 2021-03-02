import { Notifier } from '../Notifier';
import * as toastr from 'toastr';
import { Injectable } from "@angular/core";

@Injectable()
export class ToastrNotifier extends Notifier {
    defaultPositionClass: string = "toast-bottom-right";
    progressBarSetting: any = { progressBar: true };
    successDefaultOptions: any = { timeOut: 5000, positionClass: this.defaultPositionClass };
    dangerDefaultOptions: any = { timeOut: 0, extendedTimeOut: 0, closeButton: true, positionClass: this.defaultPositionClass };
    warningDefaultOptions: any = { timeOut: 0, extendedTimeOut: 0, closeButton: true, positionClass: this.defaultPositionClass };
    infoDefaultOptions: any = { timeOut: 5000, positionClass: this.defaultPositionClass };

    success(title: string, message?: string, options?: any): void {
        let configOptions: any = { ...this.successDefaultOptions, ...options };
        toastr.success(title, message, this.addProgressBarIfNotPersistent(configOptions));
    }
    danger(title: string, message?: string, options?: any): void {
        let configOptions: any = { ...this.dangerDefaultOptions, ...options };
        toastr.error(title, message, this.addProgressBarIfNotPersistent(configOptions));
    }
    warning(title: string, message?: string, options?: any): void {
        let configOptions: any = { ...this.warningDefaultOptions, ...options };
        toastr.warning(title, message, this.addProgressBarIfNotPersistent(configOptions));
    }
    info(title: string, message?: string, options?: any): void {
        let configOptions: any = { ...this.infoDefaultOptions, ...options };
        toastr.info(title, message, this.addProgressBarIfNotPersistent(configOptions));
    }

    successList(messages: string[], splitMessages: boolean = false, options?: any): void {
        let configOptions: any = { ...this.successDefaultOptions, ...options };
        if (splitMessages === true) {
            for (let i = 0; i < messages.length; i++)
                this.success(messages[i], '', configOptions);
        }
        else
            this.success(super.formatMultipleAsHtml(messages), '', configOptions);
    }
    dangerList(messages: string[], splitMessages: boolean = false, options?: any): void {
        let configOptions: any = { ...this.dangerDefaultOptions, ...options };
        if (splitMessages === true) {
            for (let i = 0; i < messages.length; i++)
                this.danger(messages[i], '', configOptions);
        }
        else
            this.danger(super.formatMultipleAsHtml(messages), '', configOptions);
    }
    warningList(messages: string[], splitMessages: boolean = false, options?: any): void {
        let configOptions: any = { ...this.warningDefaultOptions, ...options };
        if (splitMessages === true) {
            for (let i = 0; i < messages.length; i++)
                this.warning(messages[i], '', configOptions);
        }
        else
            this.warning(super.formatMultipleAsHtml(messages), '', configOptions);
    }
    infoList(messages: string[], splitMessages: boolean = false, options?: any): void {
        let configOptions: any = { ...this.infoDefaultOptions, ...options };
        if (splitMessages === true) {
            for (let i = 0; i < messages.length; i++)
                this.info(messages[i], '', configOptions);
        }
        else
            this.info(super.formatMultipleAsHtml(messages), '', configOptions);
    }

    removeAll(): void {
        toastr.clear();
    }
    removePersistents(): void {
        let toastElements = document.getElementsByClassName('toast');
        for (let i = toastElements.length - 1; i >= 0; i--) {
            const element = toastElements[i];
            if (!element.firstElementChild.classList.contains('toast-progress'))
                element.remove();
        }
    }

    addProgressBarIfNotPersistent(options: any): any {
        var configOptions: any = options;
        if (configOptions.hasOwnProperty('timeOut') && configOptions['timeOut'] > 0)
            configOptions = { ...configOptions, ...this.progressBarSetting };
        return configOptions;
    }
}