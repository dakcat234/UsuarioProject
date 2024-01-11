using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PopsyMarket.Utilities;
using UsuarioProject.Settings;

namespace PopsyMarket.Filters
{/// <summary>
 /// Filtro de acción para extender el tiempo de vida de un token en el encabezado de la respuesta.
 /// </summary>
    public class ExtenderTokenHeaderFilter : IActionFilter
    {
        private readonly TokenSettings _settings;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ExtenderTokenHeaderFilter"/>.
        /// </summary>
        /// <param name="settings">Configuración del token.</param>
        public ExtenderTokenHeaderFilter(TokenSettings settings)
        {
            this._settings = settings;
        }

        /// <summary>
        /// Método que se ejecuta antes de la ejecución de una acción en un controlador.
        /// Extiende el tiempo de vida del token en el encabezado de la respuesta.
        /// </summary>
        /// <param name="context">Contexto de ejecución de la acción.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            ControllerBase? controller = context.Controller as ControllerBase;
            if (controller is not null)
                controller.ExtenderTokenHeader(_settings);
        }

        /// <summary>
        /// Método que se ejecuta después de la ejecución de una acción en un controlador.
        /// No se realiza ninguna acción en este caso.
        /// </summary>
        /// <param name="context">Contexto de ejecución de la acción.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No se realiza ninguna acción en este caso
        }
    }
}
