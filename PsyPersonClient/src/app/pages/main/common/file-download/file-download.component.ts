import { HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProgressStatus, ProgressStatusEnum } from 'src/app/models/appFiles.models';
import { AppFilesService } from 'src/app/services/api/appFiles.serive';

@Component({
  selector: 'app-file-download',
  templateUrl: './file-download.component.html',
  styleUrls: ['./file-download.component.css']
})
export class FileDownloadComponent implements OnInit {

  @Input() disabled: boolean = false;
  @Input() fileName: string = '';
  @Input() type: string = '';
  @Input() buttonName: string = "Скачать";
  @Output() downloadStatus: EventEmitter<ProgressStatus> = new EventEmitter<ProgressStatus>();

  constructor(private service: AppFilesService) { }

  ngOnInit(): void {
  }

  download() {
    this.downloadStatus.emit( {status: ProgressStatusEnum.START});
    this.service.downloadFile(this.fileName).toPromise().then(
      data => {
        switch (data.type) {
          case HttpEventType.DownloadProgress:
            this.downloadStatus.emit( {status: ProgressStatusEnum.IN_PROGRESS, percentage: Math.round((data.loaded / (data.total ?? 1)) * 100)});
            break;
          case HttpEventType.Response:
            this.downloadStatus.emit( {status: ProgressStatusEnum.COMPLETE});
            const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });
            const a = document.createElement('a');
            a.setAttribute('style', 'display:none;');
            document.body.appendChild(a);
            a.download = this.fileName;
            a.href = URL.createObjectURL(downloadedFile);
            a.target = '_blank';
            a.click();
            document.body.removeChild(a);
            break;
        }
      },
      error => {
        this.downloadStatus.emit( {status: ProgressStatusEnum.ERROR});
      }
    );
  }

}