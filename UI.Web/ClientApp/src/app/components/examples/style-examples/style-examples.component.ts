import { Component, OnInit } from '@angular/core';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'app-style-examples',
  templateUrl: './style-examples.component.html',
  styleUrls: ['./style-examples.component.scss']
})
export class StyleExamplesComponent implements OnInit {

  constructor(private god: GodService) { }

  ngOnInit(): void {
  }

  messageList: string[] = ['first worked', 'second message', 'third message', 'testing testing', 'keep going!'];
  
  Success = () => this.god.notifications.success('Success worked');
  Danger = () => this.god.notifications.danger('Danger worked');
  Warning = () => this.god.notifications.warning('Warning worked');
  Info = () => this.god.notifications.info('Info worked', undefined, { timeOut: 0, closeButton: true });
  OverrideDefaults = () => {
    this.god.notifications.success('Success worked', undefined, { timeOut: 0, extendedTimeOut: 0, closeButton: true });
    this.god.notifications.info('Info worked', undefined, { timeOut: 0, extendedTimeOut: 0, closeButton: true });
  }

  Multiple = () => this.god.notifications.dangerList(this.messageList);
  SplitMultiple = () => this.god.notifications.infoList(this.messageList, true);
  MultipleWithOverrides = () => this.god.notifications.warningList(this.messageList, undefined, { timeOut: 30000, preventDuplicates: true, closeButton: true });

  RemoveAll = () => this.god.notifications.removeAll();
  RemovePersistent = () => this.god.notifications.removePersistents();
}
