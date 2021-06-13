export interface board{
    id: string;
    name: string;
    lists: lists[]
}

 interface lists{
    id: string;
    title: string;
    order: number,
    items: items[]

}
 interface items{
    id: string;
    title: string;
    order: number,
  

}
