export const wsServer = new signalR.HubConnectionBuilder().withUrl("/ws-server").build();
