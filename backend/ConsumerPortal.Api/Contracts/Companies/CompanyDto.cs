namespace ConsumerPortal.Api.Contracts.Companies;

public record CompanyDto(
    Guid Id, 
    string Name, 
    string Inn, 
    int Category
);
