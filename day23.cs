The Proxy Pattern is a structural design pattern that provides a placeholder or surrogate for another object to control access to it.

🔍 Real-world Analogy
Think of a security guard at a gate. The guard doesn't do the actual work inside the building but:

Checks if you have access.

Lets you in or stops you.

Here, the guard is the proxy for the building's access.

💡 Intent of Proxy Pattern
“Provide a surrogate or placeholder for another object to control access to it.”

You use a proxy when:

You want to add security, logging, lazy loading, caching, etc.

You don't want the client to interact directly with the real object.

