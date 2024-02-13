export interface TimePickerTime {
  id: number;
  title: string;
  isAvailable: boolean;
}

export const AVAILABLE_HOURS: TimePickerTime[] = [
  { id: 1, title: '08:00', isAvailable: false },
  { id: 2, title: '09:00', isAvailable: true },
  { id: 3, title: '10:00', isAvailable: true },
  { id: 4, title: '11:00', isAvailable: false },
  { id: 5, title: '12:00', isAvailable: false },
  { id: 6, title: '13:00', isAvailable: true },
  { id: 7, title: '14:00', isAvailable: false },
  { id: 8, title: '15:00', isAvailable: true },
  { id: 9, title: '16:00', isAvailable: false },
];
