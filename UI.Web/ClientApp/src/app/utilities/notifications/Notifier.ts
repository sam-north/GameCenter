import { INotify } from "./INotify";

export abstract class Notifier implements INotify {
    abstract success(title: string, message?: string, options?: any): void;
    abstract danger(title: string, message?: string, options?: any): void;
    abstract warning(title: string, message?: string, options?: any): void;
    abstract info(title: string, message?: string, options?: any): void;

    abstract successList(messages: string[], splitMessages?: boolean, options?: any): void;
    abstract dangerList(messages: string[], splitMessages?: boolean, options?: any): void;
    abstract warningList(messages: string[], splitMessages?: boolean, options?: any): void;
    abstract infoList(messages: string[], splitMessages?: boolean, options?: any): void;

    abstract removeAll(): void;
    abstract removePersistents(): void;

    protected formatMultipleAsHtml(messages: string[]) {
        let formattedMessage: string = '<ul>';
        for (let i = 0; i < messages.length; i++)
            formattedMessage += '<li>' + messages[i] + '</li>';
        return formattedMessage + '</ul>';
    }
}