namespace BackendDesafio.API.Exceptions;

public class ApplicationException(string message) : Exception(message);

public class MenuItemNotFoundException(int id) : ApplicationException($"Menu item with ID {id} does not exist.");

public class RelatedMenuItemNotFoundException(int relatedId) : ApplicationException($"Related menu item with ID {relatedId} does not exist.");