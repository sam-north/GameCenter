export interface INotify {
    success(title: string, message?: string, options?: any): void;
    danger(title: string, message?: string, options?: any): void;
    warning(title: string, message?: string, options?: any): void;
    info(title: string, message?: string, options?: any): void;

    successList(messages: string[], splitMessages?: boolean, options?: any): void;
    dangerList(messages: string[], splitMessages?: boolean, options?: any): void;
    warningList(messages: string[], splitMessages?: boolean, options?: any): void;
    infoList(messages: string[], splitMessages?: boolean, options?: any): void;

    removeAll(): void;
    removePersistents(): void;
}