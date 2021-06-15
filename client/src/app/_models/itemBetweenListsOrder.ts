export interface ItemOrderBeTweenLists {
    container: Container
    previousContainer: Container
}

interface Container {
    id: string,
    items?: any[]
}

