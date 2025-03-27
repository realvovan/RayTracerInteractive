namespace RayTracer;

class Sphere(Vector3 center,double radius,Material material) : Hittable {
	public Vector3 Center { get; set; } = center;
	public Material Material { get; set; } = material;
	public double Radius { get; set; } = Math.Max(0,radius);

	public override bool Hit(Ray r, Interval rayT, ref HitRecord rec) {
        Vector3 oc = this.Center - r.Origin;
        var a = r.Direction.LengthSquared();
        var h = Vector3.Dot(r.Direction,oc);
        var c = oc.LengthSquared() - Radius*Radius;
        var discriminant = h*h - a*c;

        if(discriminant < 0) return false;
        
        var sqrtd = Math.Sqrt(discriminant);
        var root = (h - sqrtd) / a;
        if(!rayT.Surrounds(root)) {
            root = (h + sqrtd) / a;
            if(!rayT.Surrounds(root)) return false;
        }
        rec.T = root;
        rec.P = r.At(rec.T);
        rec.SetFaceNormal(r,(rec.P - this.Center) / Radius);
        rec.Material = this.Material;
        return true;
    }
}