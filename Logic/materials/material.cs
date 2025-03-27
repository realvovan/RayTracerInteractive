namespace RayTracer;

abstract class Material {
    public abstract bool Scatter(Ray rIn,HitRecord rec,ref Color attenuation,ref Ray scattered);
}

class Lambertian(Color albedo) : Material {
    public Color Albedo { get; set; } = albedo;

    public override bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered) {
        var scatterDir = rec.Normal + Vector3.GetRandomUnitVector();
        if(scatterDir.IsNearZero()) scatterDir = rec.Normal;
        scattered = new Ray(rec.P,scatterDir);
        attenuation = this.Albedo;
        return true;
    }
}

class Metal(Color albedo,double fuzz) : Material {
    public Color Albedo { get; set; } = albedo;
    public double Fuzz { get; set; } = fuzz;

    public override bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered) {
        Vector3 reflected = Vector3.Reflect(rIn.Direction,rec.Normal);
        reflected = Vector3.ToUnit(reflected) + (this.Fuzz * Vector3.GetRandomUnitVector());
        scattered = new Ray(rec.P,reflected);
        attenuation = this.Albedo;
        return Vector3.Dot(scattered.Direction,rec.Normal) > 0;
    }
}

class Dielectric(double refractionIndex) : Material {
    public double RefractionIndex { get; set; } = refractionIndex;

    public override bool Scatter(Ray rIn, HitRecord rec, ref Color attenuation, ref Ray scattered) {
        attenuation = new Color(1,1,1);
        double ri = rec.FrontFace ? (1/this.RefractionIndex) : this.RefractionIndex;
        Vector3 unitDir = Vector3.ToUnit(rIn.Direction);
        double cosTheta = Math.Min(Vector3.Dot(-unitDir,rec.Normal),1.0);
        double sinTheta = Math.Sqrt(1.0 - cosTheta*cosTheta);
        Vector3 direction;
        if(ri * sinTheta > 1.0 || reflectance(cosTheta,ri) > Utils.RandomDouble()) direction = Vector3.Reflect(unitDir,rec.Normal);
        else direction = Vector3.Refract(unitDir,rec.Normal,ri);
        scattered = new Ray(rec.P,direction);
        return true;
    }

    static double reflectance(double cosine,double refractionIndex) {
        var r0 = (1 - refractionIndex) / (1 + refractionIndex);
        r0 = r0*r0;
        return r0 + (1-r0)*Math.Pow(1-cosine,5);
    }
}