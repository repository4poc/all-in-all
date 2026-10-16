## Run the application

```
node src/server.js
```

## Build an image

```
docker build -t my-node-app .
```

## Run the container

```
docker run -p 3000:3000 my-node-app
```

## Scenario

```
$ docker build -t my-node-app .
DEPRECATED: The legacy builder is deprecated and will be removed in a future release.
            BuildKit is currently disabled; enable it by removing the DOCKER_BUILDKIT=0
            environment-variable.
```

### Fix

```
That message is a warning, not a build failure. Your Docker build is using the old legacy builder because DOCKER_BUILDKIT=0 is set
```

```
unset DOCKER_BUILDKIT
```

OR

```
DOCKER_BUILDKIT=1 docker build -t my-node-app .
```

## List images

```
docker images
```
