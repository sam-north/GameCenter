import { Injectable } from '@angular/core';
import { Notifier } from '../utilities/notifications/Notifier';

@Injectable()
export class NotificationService {

  constructor(private notifier: Notifier) { }

  success = (title: string, message?: string, options?: any): void => this.notifier.success(title, message, options);
  danger = (title: string, message?: string, options?: any): void => this.notifier.danger(title, message, options);
  info = (title: string, message?: string, options?: any): void => this.notifier.info(title, message, options);
  warning = (title: string, message?: string, options?: any): void => this.notifier.warning(title, message, options);

  successList = (messages: string[], splitMessages?: boolean, options?: any): void => this.notifier.successList(messages, splitMessages, options);
  dangerList = (messages: string[], splitMessages?: boolean, options?: any): void => this.notifier.dangerList(messages, splitMessages, options);
  warningList = (messages: string[], splitMessages?: boolean, options?: any): void => this.notifier.warningList(messages, splitMessages, options);
  infoList = (messages: string[], splitMessages?: boolean, options?: any): void => this.notifier.infoList(messages, splitMessages, options);

  removeAll = (): void => this.notifier.removeAll();
  removePersistents = (): void => this.notifier.removePersistents();
}
