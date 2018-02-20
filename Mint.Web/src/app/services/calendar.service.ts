import { Injectable } from '@angular/core';

@Injectable()
export class CalendarService {

  constructor() {
  }

  getCurrentYear(): number {
    return (new Date()).getFullYear();
  }

  getYears(): number[] {
    let result: number[] = new Array();
    for (let i = 2010; i <= (new Date()).getFullYear(); i++) {
      result.push(i);
    }
    return result;
  }

  getCurrentMonth(): number {
    return (new Date()).getMonth();
  }

  getMonths(year: number): any[] {
    let result: any[] = new Array();
    result.push({ id: 1, name: "January" });
    result.push({ id: 2, name: "Feburary" });
    result.push({ id: 3, name: "March" });
    result.push({ id: 4, name: "April" });
    result.push({ id: 5, name: "May" });
    result.push({ id: 6, name: "June" });
    result.push({ id: 7, name: "July" });
    result.push({ id: 8, name: "August" });
    result.push({ id: 9, name: "September" });
    result.push({ id: 10, name: "October" });
    result.push({ id: 11, name: "November" });
    result.push({ id: 12, name: "December" });
    return result;
  }
}
