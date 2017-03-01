## Running tests

- `dotnet test` doesn't work with new `.csproj` projects. Waiting for the fix!

## Concourse CI

[Install Concourse CI](http://concourse.ci/installing.html)

### Known Issues
- If you get an error like the this, and you're running on Docker for mac v1.13.0, you'll need to downgrade Docker to version 1.12. See [here](https://github.com/concourse/concourse/issues/927) and [here](https://github.com/concourse/concourse/issues/896).
  ```
  docker: Error response from daemon: error creating aufs mount to /var/lib/docker/aufs/mnt/ce858db29abcc2eee80cd7ab8bdf46a96aa66b9c279f2993446937b01d19ae18-init: invalid argument.
  See 'docker run --help'
  ```

## Deploying .NET Core Apps to PCF

- CF dotnet-core-buildpacks don't support cli RC4, use this fork:

  ```
  https://github.com/denissezavala/dotnet-core-buildpack
  ```
- When deploying a solution that has multiple projects, add a `.deployment` file to specify the main project: 
  ```
  [config]
  project = src/TodoList/TodoList.csproj
  ```
- Specify the app-environment and publish-config in `manifest.yml`
  ```
  ASPNETCORE_ENVIRONMENT: production
  PUBLISH_RELEASE_CONFIG: true
  ```

### Consuming CF Services
- Use [steeltoe](https://steeltoe.io/) to connect to CF Services
- Keep local secrets in local `settings.json` and load it into the config on startup
