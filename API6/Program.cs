WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// TODO: Fai cose col builder

WebApplication app = builder.Build(); // Fine fase 1 => creo interfaccia IHOST

// TODO: Fai cose cose con app

app.MapGet("/ciao", () => "Hello World!");

app.Run(); // Fine fase 2 => start dell'applicazione