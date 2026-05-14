

export interface Manager 
{   
    id : number,
    username : string,
    passwordHash : string,
    hotelId : number
}

export interface NewManagerDTO
{
    username: string,
    passwordHash: string,   
    hotelId: number
}