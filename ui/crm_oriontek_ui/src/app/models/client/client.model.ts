export interface Client {
  clientId?: number;
  name: string;
  locations: Location[];
}

export interface Location {
  locationName: string;
  provinceName: string;
  municipalityName: string;
  districtName: string;
  sectorName: string;
}
