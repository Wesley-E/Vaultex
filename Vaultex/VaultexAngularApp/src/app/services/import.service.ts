import { Injectable } from '@angular/core';
import {Import} from "../interfaces/import";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ImportService {

  constructor(private http: HttpClient) { }

  async importFromExcel(filePath: string): Promise<void> {
    const importData: Import = {
      importType: 0,
      fileName: filePath
    }
    this.http.post<any>('html://localhost:5026/import', importData)
  }
}
