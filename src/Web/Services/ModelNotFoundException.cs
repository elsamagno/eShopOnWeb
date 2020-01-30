using System;

public class ModelNotFoundException: Exception {

        public ModelNotFoundException(string message, Exception innerException = null)
            : base(message, innerException) {

            }
    }

