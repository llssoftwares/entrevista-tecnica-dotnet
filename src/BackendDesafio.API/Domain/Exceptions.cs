namespace BackendDesafio.API.Domain;

public class DomainException(string message) : Exception(message);

public class MenuItemNotFoundException(int id) : DomainException($"Menu item with ID {id} does not exist.");

public class RelatedMenuItemNotFoundException(int relatedId) : DomainException($"Related menu item with ID {relatedId} does not exist.");